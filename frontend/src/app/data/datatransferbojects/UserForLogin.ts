export class UserForLogin{
    constructor(
        public email?: string,
        public password?: string,
        public rememberMe: boolean = false
    ){}
}