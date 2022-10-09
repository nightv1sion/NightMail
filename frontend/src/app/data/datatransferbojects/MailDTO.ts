export class MailDTO {
    constructor(
        public text: string,
        public subject: string,
        public receiverMail: string,
        public creationDateTime: Date
    ){}
}