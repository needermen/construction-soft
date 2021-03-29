import {Injectable} from "@angular/core";
import {Subject} from "rxjs";

@Injectable()
export class FullPageLoadingService {
    loading = new Subject<boolean>();

    setLoading(value : boolean){
      this.loading.next(value);
    }
}
