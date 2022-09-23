export class ClaimDTO{
    constructor(
        public userId: string,
        public exp: number,
        public iss: string,
        public aud: string,
    ){}
}