import { Component, OnInit } from '@angular/core';
import { Splitwise } from '../../api/SplitWiseApi'


@Component({
  selector: 'app-my-group',
  templateUrl: './my-group.component.html',
  styleUrls: ['./my-group.component.css']
})
export class MyGroupComponent implements OnInit {

  p: number = 1;
  group:Splitwise.GroupReturn[] = [];
  constructor(private groupService:Splitwise.GroupClient) { }

  ngOnInit(): void {
    this.groupService.groupList().subscribe(
      (data) => {
        this.group = data;
      }
    );
  }
  deleteGroup(groupData:Splitwise.GroupReturn){
    if(confirm(`Are you sure you want to delete ${groupData.name}?`))
    this.groupService.deleteGroup(groupData.groupId).subscribe(
      () => {
        console.log("Group Deleted!");
      }
    );
  }

}
