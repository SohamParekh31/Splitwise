import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Splitwise } from 'src/app/api/SplitWiseApi';


@Component({
  selector: 'app-add-friend',
  templateUrl: './add-friend.component.html',
  styleUrls: ['./add-friend.component.css']
})
export class AddFriendComponent implements OnInit {

  friend1:string = localStorage.getItem('id');
  friend2:string = null;
  friend:Splitwise.AddFriend;
  constructor(private friendService:Splitwise.FriendClient,private route:Router) { }

  ngOnInit(): void {
  }
  onSubmit(form:NgForm){
    this.friend = form.value;
    this.friendService.postFriend(this.friend).subscribe(
      ()=>{
        console.log("Friend Added Successfully!");
        this.route.navigate(['/dashboard/splitwise']);
      }
    );
  }

}
