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
      (data) => {
        this.groupDetails = data;
        this.setGroupValue(this.groupDetails);
      }
    );
  }
  setGroupValue(groupDetails: Splitwise.AddGroup){
    this.groupForm.patchValue({
      name: groupDetails.name,
      date: groupDetails.date,
      createdBy: groupDetails.createdBy,
      simplyfyDebits: groupDetails.simplyfyDebits,
      isDeleted: groupDetails.isDeleted
    });
    this.groupForm.setControl('users',this.SetUsers(groupDetails.users));
  }
  SetUsers(users):FormArray{
    const FormArr = new FormArray([]);
    FormArr.push(users.forEach((s: { name: any; email: any; }) => {
      this.fb.group({
        name: s.name,
        email:s.email
      });
    }))
    return FormArr;
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
    this.groupService.editGroup(this.ID,this.group).subscribe(
      () => {
        console.log("Group Added Successfully!");
        this.route.navigate(['/dashboard/splitwise']);
      }
    );
  }

}
