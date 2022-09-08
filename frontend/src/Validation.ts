
export function validateName(fieldName: string, value: string){
    if(!value)
        return fieldName + " is required";
    if(value[0].toLowerCase() == value[0])
        return fieldName + " must begin with a capital letter";
    return null;
}

export function validateEmail(email: string){
    if(!email)
        return "Email is required"
    if(!/^[0-9a-zA-Z]+@nightmail.com$/i.test(email))
        return "Email must end with '@nightmail.com'";
}

export function validateDate(fieldName: string, dateString: string){
    const date = new Date(dateString);
    const nowDate = new Date(Date.now());
    const minDate = new Date(new Date(nowDate).getFullYear() - 100, nowDate.getMonth(), nowDate.getDate());
    const maxDate = new Date(new Date(nowDate).getFullYear() + 100, nowDate.getMonth(), nowDate.getDate());
    console.log("dates:");
    console.log(minDate + " - "  + minDate.getTime());
    console.log(date + " - "  + nowDate.getTime());
    console.log(maxDate.getTime());
    if(date.getTime() < minDate.getTime())
        return fieldName + " is too small!";
    if(date.getTime() > maxDate.getTime())
        return fieldName + " is too big!";
}

export function validatePassword(password: string){
    if(!password)
        return "Password is required";
    if(password.length < 8)
        return "Password is too short";
    
    let hasNumber = false;
    let hasUppercaseLetter = false;
    let hasLowercaseLetter = false;
    let hasNonAlphaNumeric = false;
    for(let i = 0; i<password.length; i++){
        if(Number.isInteger(Number(password[i])))
            hasNumber = true;
        if(password[i].toUpperCase() === password[i])
            hasUppercaseLetter = true;
        if(password[i].toLowerCase() === password[i])
            hasLowercaseLetter = true;
        if(!Number.isInteger(password[i]) && password[i].toUpperCase() != password[i].toLowerCase())
            hasNonAlphaNumeric = true;
    }

    if(!hasNumber || !hasUppercaseLetter || !hasLowercaseLetter || !hasNonAlphaNumeric)
    {
        return "Password must contain lowercase and uppercase letter, digit, non-alphanumeric character";
    }
    
    return null;
    
}

export function validateMatching(confirmPassword: string | undefined, password: string){
    if(password !== confirmPassword)
        return "Passwords don't match";
}

