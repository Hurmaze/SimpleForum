export class Role {
    Id: number;
    RoleName: string;
    AccountsIds: number[];
    constructor(id:number, rolename: string, accounts: number[]){
        this.Id = id;
        this.RoleName = rolename,
        this.AccountsIds = accounts;
    }
}

