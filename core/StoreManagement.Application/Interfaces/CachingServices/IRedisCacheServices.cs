public interface IRedisCacheServices {
    T? GetData<T>(string key);
    void SetData<T>(string key, T value);

}