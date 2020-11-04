import { UserGroup } from './UserGroup';

export interface AddGroup{
  name?:string;
  createdBy?:string;
  Date?:Date;
  simplyfyDebits?:boolean;
  isDeleted?:boolean;
  userGroup?:UserGroup[];
}

