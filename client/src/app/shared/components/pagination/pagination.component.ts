import {
	Component,
	EventEmitter,
	Input,
	OnChanges,
	OnInit,
	Output,
} from "@angular/core";
import { NgFor } from "@angular/common";

@Component({
	selector: "table-pagination",
	templateUrl: "./pagination.component.html",
	styleUrls: ["./pagination.component.scss"],
	standalone: true,
	imports: [NgFor],
})
export class PaginationComponent implements OnInit, OnChanges {
	public totalPage!: number;
	public totalRecords!: number;
	public currentPage: number = 1;
	// output when onChangePage
	@Output() changePage = new EventEmitter();
	@Output() changePageSize = new EventEmitter();
	@Input() displayEdit: boolean = false;
	@Input()
	_totalPage!: number;
	@Input()
	_totalRecords!: number;
	@Input()
	_currentPage!: number;
	@Input() _pageSize: number = 5;
	@Input() _hasNext: boolean = true;
	@Input() _hasPrev: boolean = false;
	// render Array Page
	@Input()
	_hasPageSize!: boolean;
	totalPageArr: number[] = [1, 2, 3, 4];
	@Input() _pageSizeArr: number[] = [10, 15];

	ngOnInit(): void {
		this.totalPageArr = this.convertArrayPage();
	}

	ngOnChanges(): void {
		this.totalPage = this._totalPage;
		this.totalRecords = this._totalRecords;
		this.currentPage = this._currentPage;
		this.totalPageArr = this.convertArrayPage();
	}

	convertArrayPage(): any[] {
		const pagination = [];
		const visiblePages = 5;
		let startPage = Math.max(
			1,
			this.currentPage - Math.floor(visiblePages / 2) - 1
		);
		let endPage = Math.min(this.totalPage, startPage + visiblePages - 1);

		if (startPage > 1) {
			pagination.push("...");
		}

		for (let i = startPage; i <= endPage; i++) {
			pagination.push(i);
		}

		if (endPage < this.totalPage) {
			pagination.push("...");
		}

		return pagination;
	}

	// Event Change Page -> Output() to parent Component
	onPageChange(page: any): void {
		if (typeof page === "number") {
			switch (page) {
				// when current page = 1
				case 0: {
					this.currentPage = 1;
					break;
				}
				// when current page = total page
				case this.totalPage + 1: {
					this.currentPage = this.totalPage;
					break;
				}
				default: {
					this.currentPage = page;
					this.totalPageArr = this.convertArrayPage();
					this.changePage.emit(this.currentPage);
				}
			}
		}
	}
}
