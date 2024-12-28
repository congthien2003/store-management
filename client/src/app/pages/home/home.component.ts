import { Component, OnInit } from "@angular/core";
import { NgxSpinnerService } from "ngx-spinner";
import { FooterComponent } from "../layout/footer/footer.component";
import { NgFor } from "@angular/common";
import { HeaderComponent } from "../layout/header/header.component";
import { CarouselComponent } from "./carousel/carousel.component";
import {
	trigger,
	transition,
	style,
	animate,
	state,
} from "@angular/animations";

@Component({
	selector: "app-home",
	templateUrl: "./home.component.html",
	styleUrls: ["./home.component.scss"],
	standalone: true,
	imports: [HeaderComponent, NgFor, FooterComponent, CarouselComponent],
	animations: [
		trigger("fadeInUp", [
			transition(":enter", [
				style({ opacity: 0, transform: "translateY(20px)" }),
				animate(
					"0.6s ease-out",
					style({ opacity: 1, transform: "translateY(0)" })
				),
			]),
		]),
		trigger("slideInRight", [
			transition(":enter", [
				style({ opacity: 0, transform: "translateX(30px)" }),
				animate(
					"0.8s ease-out",
					style({ opacity: 1, transform: "translateX(0)" })
				),
			]),
		]),
	],
})
export class HomeComponent {
	constructor() {}
	title = "carousel";
	images = [
		{
			imageSrc:
				"https://www.webfox.dev/assets/img/casestudy/on-demand-food-delivery-web-solution/image-10.webp",
			imageAlt: "picture1",
		},
		{
			imageSrc:
				"https://lptech.asia/uploads/files/2021/04/09/Thiet-ke-web-ban-do-an-1.png",
			imageAlt: "picture2",
		},
		{
			imageSrc:
				"https://cdn.cssauthor.com/wp-content/uploads/2023/06/Delicious.jpg?strip=all&lossy=1&ssl=1",
			imageAlt: "picture3",
		},
		{
			imageSrc:
				"https://mayurik.com/uploads/P7087/Bakery%20management%20system%20project%20in%20php%20with%20source%20code.jpg",
			imageAlt: "picture4",
		},
	];
	activeIndex: number | null = null;
	questions = [
		{
			title: "Phần mềm quản lý cửa hàng là gì?",
			content:
				"Phần mềm quản lý cửa hàng (POS) là phần mềm hỗ trợ xử lý các giao dịch bán hàng và chấp nhận thanh toán trực tiếp. Với phần mềm quản lý cửa hàng StoreManagement, nhà bán hàng có thể quản lý mọi hoạt động bán hàng tại cửa hàng: tạo đơn và thanh toán cho khách hàng, quản lý hàng hóa theo danh mục, quản lý nhân viên, thu chi trong ca, quản lý tồn kho, chính sách giá linh hoạt cho đại lý, chương trình khuyến mãi đa dạng,...",
		},
		{
			title: "Những tính năng cần thiết của một phần mềm quản lý cửa hàng tốt?",
			content:
				"Một phần mềm quản lý cửa hàng tốt có thể đáp ứng đầy đủ nhu cầu của khách hàng khi bán hàng như: Quản lý sản phẩm, quản lý kho hàng, quản lý khách hàng, quản lý nhân viên, quản lý các hoạt động bán hàng, báo cáo doanh thu, lãi lỗ chi tiết, đồng bộ bán hàng đa kênh từ offline đến online trên một hệ thống xử lý bài bản...",
		},
		{
			title: "Tôi có được dùng thử trước khi mua phần mềm quản lý cửa hàng không?",
			content:
				"Bạn chỉ cần bấm vào nút Dùng thử và điền đầy đủ thông tin, bạn có thể dùng thử miễn phí phần mềm quản lý cửa hàng StoreManagement trong 14 ngày...",
		},
		{
			title: "Có đội ngũ hỗ trợ tôi khi dùng phần mềm quản lý cửa hàng không?",
			content:
				"Đội hỗ trợ khách hàng của StoreManagement sẽ luôn hỗ trợ tận tình cho bạn trong suốt quá trình sử dụng các phần mềm. Hỗ trợ miễn phí cho bạn tất cả các ngày trong tuần từ T2-CN, từ 8h00 - 22h00, giải đáp nhanh mọi thắc mắc của khách hàng qua các kênh Chat, Email, Phone...",
		},
	];

	toggleAccordion(index: number) {
		// If the same index is clicked, close it, otherwise open the clicked one
		this.activeIndex = this.activeIndex === index ? null : index;
	}
}
