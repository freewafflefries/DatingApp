import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';



@Injectable({
  providedIn: 'root'
})

export class MembersService {

  baseUrl = environment.apiUrl;
  members: Member[] = [];


  constructor(private http: HttpClient) { }

  getMembers() {
    if (this.members.length > 0) return of(this.members);

    return this.http.get<Member[]>(this.baseUrl + 'Users').pipe(
      map(members => {
        this.members = members;
        return members;
      })
    )
  }

  getMember(username) {
    const member = this.members.find(x => x.userName === username)
    if (member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'Users/byname/' + username)
  }

  updateMember(member: Member) {



    return this.http.put(this.baseUrl + 'Users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member)
        this.members[index] = member;
      })
    )
  }
}
