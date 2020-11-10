import { Component, OnInit } from '@angular/core';
import { Splitwise } from 'src/app/api/SplitWiseApi';
import { FormGroup, FormBuilder, FormArray, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-group-edit',
  templateUrl: './group-edit.component.html',
  styleUrls: ['./group-edit.component.css']
})
export class GroupEditComponent implements OnInit {


  group:Splitwise.AddGroup;
  groupForm: FormGroup;
  ID:number;
  groupDetails:Splitwise.AddGroup;
  constructor(private activatedRoute:ActivatedRoute,private fb: FormBuilder,private groupService:Splitwise.GroupClient,private route:Router) { }

  get users(){
    return <FormArray>this.groupForm.get('users');
  }
  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(
      params => {
        this.ID = +params.get('id');
        this.getGroupById(this.ID);
      }
    )
    this.groupForm = this.fb.group({
      groupId:0,
      name:'',
      date:'',
      createdBy:'',
      users:this.fb.array([this.Addmember()]),
      simplyfyDebits:false,
      isDeleted:false,
    });
  }

  getGroupById(id: number){
    this.groupService.getGroupBasedOnId(id).subscribe(
      (data:Splitwise.AddGroup) => {
        this.groupForm = this.fb.group({
          groupId: data.groupId,
          name: data.name,
          date: data.date,
          createdBy: data.createdBy,
          simplyfyDebits: data.simplyfyDebits,
          isDeleted: data.isDeleted,
          users:this.fb.array(data.users.map(datum => this.SetUsers(datum)))
        });
      }
    );
  }

  SetUsers(datum) {
    return this.fb.group({
      email: this.fb.control(datum.email),
      name: this.fb.control(datum.name)
    });
  }

  addGroupMember(){
    this.users.push(this.Addmember());
  }
  removeGroupMember(index:number){
    this.users.removeAt(index);
  }
  Addmember(): FormGroup{
    return this.fb.group({
      name:'',
      email:''
    })
  }
  onSubmit(){
    this.group = this.groupForm.value;
    this.groupService.editGroup(this.ID,this.group).subscribe(
      () => {
        console.log("Group Edited Successfully!");
        this.route.navigate(['/dashboard/splitwise']);
      }
    );
  }

}
