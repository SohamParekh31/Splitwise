import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Splitwise } from 'src/app/api/SplitWiseApi';


@Component({
  selector: 'app-friend-detail',
  templateUrl: './friend-detail.component.html',
  styleUrls: ['./friend-detail.component.css']
})
export class FriendDetailComponent implements OnInit {

  ID:string;
  friendDetail:Splitwise.UserModel;
  constructor(private activatedRoute:ActivatedRoute,private friendService:Splitwise.FriendClient,private route:Router) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(
      params => {
        this.ID = params.get('id');
        this.getFriendDetail(this.ID);
      }
    )
  }
  getFriendDetail(ID){
    this.friendService.getFriendDetail(ID).subscribe(
      (data) => {
        this.friendDetail = data;
      }
    );
  }
  DeleteFriend(user:Splitwise.ApplicationUser){
    if(confirm(`Are you sure, You want to Delete ${user?.firstName} as Friend ?`)){
      this.friendService.deleteFriend(user?.id).subscribe(
        () => {
          console.log("Friend Deleted!");
          this.route.navigate(['/dashboard/splitwise']);
        }
      );
    }
  }
}
