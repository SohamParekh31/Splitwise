import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Splitwise } from '../../api/SplitWiseApi'


@Component({
  selector: 'app-group-detail',
  templateUrl: './group-detail.component.html',
  styleUrls: ['./group-detail.component.css']
})
export class GroupDetailComponent implements OnInit {

  ID:number;
  groupDetails:any | undefined;
  constructor(private groupService:Splitwise.GroupClient,private activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(
      params => {
        this.ID = +params.get('id');
        this.getGroupById(this.ID);
      }
    )
  }
  getGroupById(id){
    this.groupService.getGroupBasedOnId(id).subscribe(
      (data) => {
        this.groupDetails = data;
        console.log(this.groupDetails);
      }
    );
  }

}
