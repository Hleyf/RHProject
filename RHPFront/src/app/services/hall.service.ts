import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable, catchError, firstValueFrom, of } from 'rxjs';
import { IHall } from '../models/hall.model';
import { API_URL } from '../shared/constants';

@Injectable({
  providedIn: 'root'
})
export class HallService {

  constructor(private http: HttpClient) { }

  halls = signal<IHall[] | []>([]);
  hallsLoading = signal<boolean>(false);
  selectedHall = signal<IHall | null>(null);

  getHalls() {
    console.log('getting halls');
    this.hallsLoading.set(true);
    this.http.get<IHall[]>(API_URL + '/hall').pipe(
      catchError((error) => {
        console.error(error);
        return of([])
      })
    ).subscribe((data: IHall[]) => {
      console.log('got halls');
      this.halls.set(data);
      this.hallsLoading.set(false);
    });
  }

  getHall(id: number) {
    console.log('getting hall');
    this.http.get<IHall>(API_URL + '/hall/' + id).pipe(
      catchError((error) => {
        console.error(error);
        return of(null)
      })
    ).subscribe((data : IHall | null) => {
      console.log('got hall');
      this.selectedHall.set(data);
      console.log('hall: ',data);
    });
  }

  async createHall(hall: IHall): Promise<number> {
    let status: number = 0;
    const response$ = this.http.post<IHall>(API_URL + '/hall', hall, {observe: 'response'});

    try{
      const res: HttpResponse<IHall> = await firstValueFrom(response$);
      status = res.status;
    }catch(err: any){
      console.error(err);
      status = err.status;
    }
    return status;
  }
}
