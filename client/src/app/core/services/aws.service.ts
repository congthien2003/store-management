import { Injectable } from '@angular/core';

import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/interfaces/Common/ApiResponse';
import { AWSApi } from '../constant/api/aws.api';
import { MasterService } from './master/master.service';
@Injectable({
  providedIn: 'root',
})
export class AWSService {
  endpoint = AWSApi;
  constructor(private service: MasterService) {}
  sendMail(
    recipientEmail: string,
    subject: string,
    body: string
  ): Observable<ApiResponse> {
    const params = new HttpParams()
      .set('RecipientEmail', recipientEmail.toString())
      .set('Subject', subject.toString())
      .set('Body', body.toString());
    return this.service.post(`${this.endpoint.sendMail}`, { params });
  }
  sendMailThanks(recipientEmail: string): Observable<ApiResponse> {
    return this.service.post(
      `${this.endpoint.sendMailThanks}`,
      JSON.stringify(recipientEmail)
    );
  }
  sendMailWelcome(
    recipientEmail: string,
    Name: string
  ): Observable<ApiResponse> {
    const params = new HttpParams()
      .set('RecipientEmail', recipientEmail.toString())
      .set('Name', Name.toString());
    return this.service.post(`${this.endpoint.sendMailWelcome}`, { params });
  }
  sendMailReportMonth(
    recipientEmail: string,
    idStore: number,
    startDate: string,
    endDate: string
  ): Observable<ApiResponse> {
    const params = new HttpParams()
      .set('RecipientEmail', recipientEmail.toString())
      .set('IdStore', idStore.toString())
      .set('StartDate', startDate.toString())
      .set('EndDate', endDate.toString());
    return this.service.post(`${this.endpoint.sendMailReportMonth}`, {
      params,
    });
  }
}
