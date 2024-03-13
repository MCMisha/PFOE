import {HttpHeaders} from "@angular/common/http";

export const httpOptions = {
  headers: new HttpHeaders({
    'accept': '*/*',
    'Content-Type': 'application/json',
  })
}
