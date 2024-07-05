import { Injectable } from "@angular/core";
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
  export class ContactListService {
        isCollapsed : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);


        toggleCollapsed() {
            this.isCollapsed.next(!this.isCollapsed.value);
        }

        setCollapsed(value: boolean) {
            this.isCollapsed.next(value);
        }
}



        
  