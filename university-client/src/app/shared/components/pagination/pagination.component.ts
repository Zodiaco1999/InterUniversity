import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { DataTableServiceBase } from '../../models/data-table-service-base';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {
  @Input() service: DataTableServiceBase<any> | null = null;
  @Input() pageSizes = [10, 20, 50, 100];
  @Input() maxSize = 10;
  @Input() size: 'sm' | 'md' | 'lg' = 'sm';
  @Input() totalRecordsObs = new Observable<number>();
  @Input() boundaryLinks = false;

  totalRecords = 0;
  recordNumber = 0;
  recordMaxPage = 10;

  ngOnInit() {
    this.service?.totalRecords$.subscribe((v) => {
      this.totalRecords = v;
      if (this.service) {
        this.recordNumber = (this.service.page - 1) * this.service.pageSize + 1;
        const records = this.service.page * this.service.pageSize;
        this.recordMaxPage = records > this.totalRecords ? this.totalRecords : records;
      }
    });
  }
}
