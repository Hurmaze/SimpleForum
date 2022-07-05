export class User {
    id: number;
    email: string;
    nickname: string='';
    threadsIds: number[];
    postsIds: number[];
    roleName: string='';
    roleId: number=0;
    constructor(id:number, email: string, nickname: string, threads: number[], posts: number[], roleName:string, roleId: number=0){
        this.id = id;
        this.email = email,
        this.nickname = nickname;
        this.threadsIds = threads;
        this.postsIds = posts;
        this.roleName = roleName;
        this.roleId = roleId;
    }
}
