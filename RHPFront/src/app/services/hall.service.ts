import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable, catchError, filter, firstValueFrom, map, of } from 'rxjs';
import { Hall, IHall } from '../models/hall.model';
import { API_URL } from '../shared/constants';

@Injectable({
  providedIn: 'root'
})
export class HallService {

  constructor(private http: HttpClient) { }

  halls = signal<IHall[] | []>([]);
  selectedHall = signal<Hall | null >(null);
  //Loaders for the hall list and hall view
  isHallsLoading = signal<boolean>(false);
  isHallLoading = signal<boolean>(false);

  getHalls() {
    console.log('getting halls');
    this.isHallsLoading.set(true);
    this.http.get<IHall[]>(API_URL + '/hall').pipe(
      catchError((error) => {
        console.error(error);
        return of([])
      })
    ).subscribe((data: IHall[]) => {
      console.log('got halls');
      this.halls.set(data);
      this.isHallsLoading.set(false);
    });
  }

  getHall(id: number) {
    console.log('getting hall');
    this.isHallLoading.set(true);
    this.http.get<IHall>(API_URL + '/hall/' + id).pipe(
      map((data: IHall) => {
        if (data?.id === id) {
          return new Hall(data);
        } else {
          throw new Error('Hall not found');
        }
      }),
      catchError((error) => {
        console.error(error);
        throw error;
      })
    ).subscribe({
      next: (hall: Hall) => {
        this.selectedHall.set(hall);
        this.isHallLoading.set(false);
      },
      error: (error) => {
        console.error('Error fetching hall:', error);
        this.isHallLoading.set(false);
        this.selectedHall.set(null);
      }
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
