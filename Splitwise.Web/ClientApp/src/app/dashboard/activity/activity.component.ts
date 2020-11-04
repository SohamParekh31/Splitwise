import { Component, OnInit } from '@angular/core';
import { Splitwise } from '../../api/SplitWiseApi'

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})
export class ActivityComponent implements OnInit {

  constructor(private activityService:Splitwise.ActivityClient) { }

  ngOnInit(): void {
    this.activityService.activityList().subscribe(
      (data) => {console.log(data);}
    );
  }

}
