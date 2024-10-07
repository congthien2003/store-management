import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Pagination } from "src/app/core/models/common/Pagination";

// Import Components
import { PaginationComponent } from "../pagination/pagination.component";

// Import Material UI
import { MatRadioModule } from "@angular/material/radio";
import { MatButtonModule } from "@angular/material/button";
import { ModalDeleteComponent } from "src/app/shared/components/modal-delete/modal-delete.component";
import { MatDialog } from "@angular/material/dialog";
import { MatTableModule } from "@angular/material/table";
import { MatDialogModule } from "@angular/material/dialog";

const MatImport = [
	MatRadioModule,
	MatButtonModule,
	MatTableModule,
	MatDialogModule,
];

@Component({
	selector: "app-table-pagi",
	standalone: true,
	imports: [CommonModule, MatImport, PaginationComponent],
	templateUrl: "./table-pagi.component.html",
	styleUrls: ["./table-pagi.component.scss"],
})
export class TablePagiComponent implements OnInit {
	@Input() config: any;
	@Input() displayedColumns!: string[];
	@Input() data: any;
	@Input() pagi!: Pagination;
	@Output() handleDelete = new EventEmitter<any>();

	constructor(private dialog: MatDialog) {}
	ngOnInit(): void {}

	onChangePage(currentPage: any): void {
		console.log(currentPage);
		this.pagi.currentPage = currentPage;
	}

	openDeleteDialog(id: number): void {
		const dialogRef = this.dialog.open(ModalDeleteComponent, {
			data: { id: id },
		});
		dialogRef.afterClosed().subscribe((result) => {
			if (result === true) {
				this.handleDelete.emit(id);
			}
		});
	}
}
