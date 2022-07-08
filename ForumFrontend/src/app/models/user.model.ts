export class User {
    id: number;
    email: string;
    nickname: string='';
    roleName: string='';
    roleId: number=0;
    constructor(id:number, email: string, nickname: string, roleName:string, roleId: number=0){
        this.id = id;
        this.email = email,
        this.nickname = nickname;
        this.roleName = roleName;
        this.roleId = roleId;
    }
}
