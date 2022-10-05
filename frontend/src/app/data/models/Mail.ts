export class Mail {
    constructor(
        public mailId: string,
        public text: string,
        public subject: string ,
        public receiverMail: string,
        public senderMail: string,
        public creationDateTime: Date,
        public IsSent: boolean
    ){}
}