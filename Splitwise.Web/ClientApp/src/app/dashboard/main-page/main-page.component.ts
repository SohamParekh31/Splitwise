import { Component, OnInit } from '@angular/core';
import { Splitwise } from 'src/app/api/SplitWiseApi';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {

  friend:Splitwise.Friend[] = [];
  userModel:Splitwise.UserModel;
  constructor(private friendService:Splitwise.FriendClient,
            private accountService:Splitwise.AccountClient) { }

  ngOnInit(): void {
    this.accountService.getUserInfo().subscribe(
      (data) => {
        this.userModel = data;
        // console.log(this.userModel);
      }
    );
    this.friendService.getFriendList().subscribe(
      (data) => {
        this.friend = data
      }
    );
  }

}
