export class Role {
    id: number;
    roleName: string;
    accountsIds: number[];
    constructor(id:number, rolename: string, accounts: number[]){
        this.id = id;
        this.roleName = rolename,
        this.accountsIds = accounts;
    }
}

