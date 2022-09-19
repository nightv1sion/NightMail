export class UserForRegistrationDto {
    constructor(
        public firstName?: string, 
        public lastName?: string,
        public birthday?: Date,
        public email?: string,
        public password?: string,
        public confirmPassword?: string
    ){}
}