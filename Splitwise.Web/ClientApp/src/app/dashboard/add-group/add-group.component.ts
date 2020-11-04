import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';



@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css']
})
export class AddGroupComponent implements OnInit {


  groupForm: FormGroup;
  constructor(private fb: FormBuilder) { }

  get users(){
    return <FormArray>this.groupForm.get('users');
  }
  ngOnInit(): void {
    this.groupForm = this.fb.group({
      name:'',
      date:'',
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
    console.log(this.groupForm.value);
  }
}

