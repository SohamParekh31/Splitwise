import { Component, OnInit } from '@angular/core';
import { Splitwise } from '../../api/SplitWiseApi'


@Component({
  selector: 'app-my-group',
  templateUrl: './my-group.component.html',
  styleUrls: ['./my-group.component.css']
})
export class MyGroupComponent implements OnInit {

  group:Splitwise.GroupReturn[] = [];
  constructor(private groupService:Splitwise.GroupClient) { }

  ngOnInit(): void {
    this.groupService.groupList().subscribe(
      (data) => {
        this.group = data;
        console.log(this.group);
      }
    );
  }

}
