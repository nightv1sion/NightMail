export interface UserForRegistration {
    firstName?: string;
    lastName?: string;
    birthday: Date | undefined;
    email?: string;
    password?: string;
    confirmPassword?: string;
}