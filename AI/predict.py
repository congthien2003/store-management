from flask import Flask, request, jsonify
import joblib
import pandas as pd
import numpy as np
from xgboost import XGBRegressor
import xgboost as xgb
from mlxtend.frequent_patterns import apriori, association_rules
app = Flask(__name__)


def predict_next_day_revenue(historical_data, model):
    """
    Dự đoán doanh thu cho ngày kế tiếp sau ngày cuối cùng trong dữ liệu lịch sử.

    historical_data: DataFrame chứa dữ liệu lịch sử (có cột 'date', 'daily_revenue', 'avg_item_price', 'quantity', 'total_orders', ...)
    model: Mô hình đã được huấn luyện (XGBoost, hoặc các mô hình khác)

    Trả về dự đoán doanh thu cho ngày kế tiếp.
    """

    # Chuyển cột 'date' của historical_data thành kiểu datetime nếu chưa
    historical_data['date'] = pd.to_datetime(historical_data['date'])

    # Lấy ngày cuối cùng trong dữ liệu lịch sử
    last_day = historical_data['date'].max()

    # Ngày kế tiếp (dự đoán doanh thu cho ngày này)
    next_day = last_day + pd.Timedelta(days=1)

    # Lấy các dữ liệu lịch sử trước ngày kế tiếp để tính các đặc trưng
    history_before_next_day = historical_data[historical_data['date'] < next_day].copy()

    # Tính toán các đặc trưng lag và rolling mean cho các ngày trước ngày kế tiếp
    for lag in range(1, 8):
        history_before_next_day[f'revenue_lag_{lag}'] = history_before_next_day['daily_revenue'].shift(lag)
        history_before_next_day[f'avg_item_price_lag_{lag}'] = history_before_next_day['avg_item_price'].shift(lag)
        history_before_next_day[f'quantity_lag_{lag}'] = history_before_next_day['quantity'].shift(lag)
        history_before_next_day[f'total_orders_lag_{lag}'] = history_before_next_day['total_orders'].shift(lag)

    # Tính rolling mean cho 7 ngày
    history_before_next_day['revenue_rolling_mean_7'] = history_before_next_day['daily_revenue'].rolling(window=7).mean()
    history_before_next_day['avg_item_price_rolling_mean_7'] = history_before_next_day['avg_item_price'].rolling(window=7).mean()
    history_before_next_day['quantity_rolling_mean_7'] = history_before_next_day['quantity'].rolling(window=7).mean()
    history_before_next_day['total_orders_rolling_mean_7'] = history_before_next_day['total_orders'].rolling(window=7).mean()

    # Tính toán log transform cho các đặc trưng
    history_before_next_day['log_avg_item_price'] = np.log1p(history_before_next_day['avg_item_price'])
    history_before_next_day['log_quantity'] = np.log1p(history_before_next_day['quantity'])

    # Thêm cột day_of_week
    history_before_next_day['day_of_week'] = history_before_next_day['date'].dt.weekday

    # Lấy dữ liệu của ngày cuối cùng trước ngày kế tiếp (dữ liệu gần nhất)
    last_day_data = history_before_next_day.iloc[-1:]

    # Kiểm tra xem dữ liệu này có đầy đủ các đặc trưng cần thiết không (nếu không, bổ sung thêm)
    if last_day_data.isnull().any().any():
        print("Dữ liệu thiếu cho ngày này, không thể dự đoán.")
        return None

    # Sử dụng mô hình để dự đoán doanh thu cho ngày kế tiếp
    predicted_revenue = model.predict(last_day_data.drop(columns=['date', 'daily_revenue']))

    # Trả về kết quả dự đoán
    return predicted_revenue[0]

def predict_and_update_history(historical_data, model, num_days=5):
    """
    Dự đoán doanh thu cho ngày tiếp theo, thêm vào lịch sử dữ liệu và dự đoán cho các ngày tiếp theo.

    historical_data: DataFrame chứa dữ liệu lịch sử (có cột 'date', 'daily_revenue', 'avg_item_price', 'quantity', 'total_orders', ...)
    model: Mô hình đã được huấn luyện (XGBoost, hoặc các mô hình khác)
    num_days: Số ngày cần dự đoán, mặc định là 5 ngày.

    Trả về lịch sử dữ liệu được cập nhật với doanh thu dự đoán cho các ngày tiếp theo.
    """

    # Chuyển cột 'date' của historical_data thành kiểu datetime nếu chưa
    historical_data['date'] = pd.to_datetime(historical_data['date'])

    # Lặp qua các ngày dự đoán
    for _ in range(num_days):
        # Dự đoán doanh thu cho ngày kế tiếp
        predicted_revenue = predict_next_day_revenue(historical_data, model)

        if predicted_revenue is None:
            print("Không thể dự đoán doanh thu cho ngày tiếp theo.")
            return historical_data

        # Lấy ngày kế tiếp sau ngày cuối cùng trong dữ liệu lịch sử
        last_day = historical_data['date'].max()
        next_day = last_day + pd.Timedelta(days=1)

        # Tạo dữ liệu giả cho ngày mới với doanh thu dự đoán
        new_data = {
            'date': next_day,
            'daily_revenue': predicted_revenue,
            'avg_item_price': historical_data['avg_item_price'].rolling(window=7).mean().iloc[-1],  # Trung bình 7 ngày gần nhất
            'quantity': historical_data['quantity'].rolling(window=7).mean().iloc[-1],  # Trung bình 7 ngày gần nhất
            'total_orders': historical_data['total_orders'].rolling(window=7).mean().iloc[-1],  # Trung bình 7 ngày gần nhất
        }

        # Chuyển dữ liệu này thành DataFrame
        new_data_df = pd.DataFrame([new_data])

        # Thêm dòng dữ liệu vào historical_data
        historical_data = pd.concat([historical_data, new_data_df], ignore_index=True)

        # Tính toán lại các đặc trưng cho dữ liệu mới (lag, rolling, log transform)
        # historical_data = add_features(historical_data)  # Hàm add_features là hàm tính toán lag, rolling, log, day_of_week

    return historical_data

def add_features(df):
    """
    Tính toán các đặc trưng lag, rolling mean, log transform cho DataFrame
    """
    # Tính toán các đặc trưng lag và rolling mean cho các ngày
    for lag in range(1, 8):
        df[f'revenue_lag_{lag}'] = df['daily_revenue'].shift(lag)
        df[f'avg_item_price_lag_{lag}'] = df['avg_item_price'].shift(lag)
        df[f'quantity_lag_{lag}'] = df['quantity'].shift(lag)
        df[f'total_orders_lag_{lag}'] = df['total_orders'].shift(lag)

    # Tính rolling mean cho 7 ngày
    df['revenue_rolling_mean_7'] = df['daily_revenue'].rolling(window=7).mean()
    df['avg_item_price_rolling_mean_7'] = df['avg_item_price'].rolling(window=7).mean()
    df['quantity_rolling_mean_7'] = df['quantity'].rolling(window=7).mean()
    df['total_orders_rolling_mean_7'] = df['total_orders'].rolling(window=7).mean()

    # Tính toán log transform cho các đặc trưng
    df['log_avg_item_price'] = np.log1p(df['avg_item_price'])
    df['log_quantity'] = np.log1p(df['quantity'])

    # Thêm cột day_of_week
    df['day_of_week'] = df['date'].dt.weekday

    return df

def process_combo(df):
    # Tạo bảng giao dịch với các món pizza đã đặt trong cùng một order_id
    basket = df.groupby(['idOrder', 'idFood']).size().unstack().reset_index().fillna(0).set_index('idOrder')
    
    # Chuyển dữ liệu sang dạng binary (1 nếu món ăn có trong đơn hàng, 0 nếu không)
    basket = basket.apply(lambda x: x.map(lambda y: 1 if y > 0 else 0))

    print(basket.head())
    # Áp dụng Apriori để tìm các combo món ăn phổ biến
	frequent_itemsets = apriori(basket, min_support=0.15, use_colnames=True)

    # Log thông tin
    print("Frequent itemsets shape:", frequent_itemsets.shape)
    
    if frequent_itemsets.empty:
        print("No frequent itemsets found.")
        return pd.DataFrame()  # Hoặc xử lý phù hợp
    # Tính số lượng itemsets
    num_itemsets = len(frequent_itemsets)

    # Tìm các quy tắc kết hợp từ các itemsets
    rules = association_rules(frequent_itemsets, num_itemsets,  metric="lift", min_threshold=1.0)

    min_confidence = 0.8
    filtered_rules = rules[rules['confidence'] >= min_confidence]

    # Tạo danh sách các combo để trả về
    combo_data = []
    for _, rule in filtered_rules.iterrows():
        combo = list(rule['antecedents']) + list(rule['consequents'])
        confidence = rule['confidence']
        combo_data.append({"combo": combo, "confidence": confidence})

    # Chuyển danh sách combo thành DataFrame để dễ dàng xử lý hơn
    combo_df = pd.DataFrame(combo_data)

    # Sắp xếp theo độ tin cậy từ cao đến thấp
    combo_df = combo_df.sort_values(by="confidence", ascending=False)
    return combo_df.head()

# Load model đã huấn luyện



# model = xgb.XGBClassifier().get_booster().load_model('best_xgb_model.pkl')
# API endpoint để nhận dữ liệu và dự đoán
@app.route('/predict', methods=['POST'])
def predict():
    history_data = pd.read_csv('data_daily_revenue.csv', index_col=0)

    # Tạo một mô hình mới
    model = joblib.load('best_xgb_model.pkl')
    numdays = request.json['numdays']
    print(numdays)
    updated_history = predict_and_update_history(history_data, model, num_days=numdays)
    
    # Giả sử updated_history là DataFrame bạn đã có
    # Cắt lấy cột 'date' và 'daily_revenue'
    test = ['date', 'daily_revenue']

    # Lấy numdays dòng cuối và chuyển đổi thành định dạng JSON
    result = updated_history[test].tail(numdays)

    # Đổi tên các cột thành 'Date' và 'Revenue'
    result = result.rename(columns={'date': 'Date', 'daily_revenue': 'Revenue'})

    # Chuyển đổi DataFrame thành JSON
    json_result = result.to_dict(orient='records')

    # In kết quả JSON
    print(json_result)

    # Trả về kết quả dưới dạng JSON
    return jsonify({'prediction': json_result})

@app.route('/get_popular_combos', methods=['POST'])
def get_popular_combos():
    # Load data from supabase
    json_data = request.json['data']

    df = pd.DataFrame(json_data)

    # Xử lý
    combo_df = process_combo(df)
    # Trả về dữ liệu combo dưới dạng JSON
    return jsonify(combo_df.to_dict(orient="records"))


if __name__ == '__main__':
    app.run(debug=True, port=3253)
