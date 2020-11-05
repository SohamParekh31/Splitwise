import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { Splitwise } from 'src/app/api/SplitWiseApi';



@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css']
})
export class AddGroupComponent implements OnInit {

  group:Splitwise.AddGroup;
  groupForm: FormGroup;
  id:string = localStorage.getItem('id');
  constructor(private fb: FormBuilder,private groupService:Splitwise.GroupClient,private route:Router) { }

  get users(){
    return <FormArray>this.groupForm.get('users');
  }
  ngOnInit(): void {
    this.groupForm = this.fb.group({
      name:'',
      date:'',
      createdBy:this.id,
      users:this.fb.array([this.Addmember()]),
      simplyfyDebits:false,
      isDeleted:false,
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
    console.log(this.group);
    this.groupService.postGroup(this.group).subscribe(
      () => {
        console.log("Group Added Successfully!");
        this.route.navigate(['/dashboard/splitwise']);
      }
    );
  }
}

