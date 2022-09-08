import { Field, Form, Formik, useFormik } from "formik"
import { UserForRegistration } from "../interfaces";
import * as Yup from "Yup";
import { validateName, validateEmail, validateMatching, validatePassword, validateDate } from "../Validation";
import AuthenticationField from "./AuthenticationField";



export default function Register(){
    let initialValues:UserForRegistration = {
        firstName: undefined,
        lastName: undefined,
        birthday: undefined,
        email: undefined,
        password: undefined,
        confirmPassword: undefined,
    };

    

    const handleForm = (values: UserForRegistration) => {
        console.log(values);
    }

    const formik = useFormik<UserForRegistration>({initialValues: initialValues, onSubmit: handleForm});

    return <div className = "register">
        <Formik initialValues={initialValues} onSubmit = {handleForm}>
            {({values, errors, touched, isValidating}) => (
                <Form className = "register-form">
                        <AuthenticationField name="firstName" validate={(value: string) => validateName("First Name", value) } 
                            labelName={"First Name"} touched={touched.firstName ? true : false} error = {errors.firstName} />

                        <AuthenticationField name="lastName" validate={(value: string) => validateName("Last Name", value) }
                            labelName={"Last Name"} touched={touched.lastName ? touched.lastName : false} error={errors.lastName} />
                        
                        <AuthenticationField type="date" name="birthDay" validate={(date: string) => validateDate("Birth Day", date)} 
                            labelName={"Birth day"} touched={touched.birthday ? touched.birthday : false} error={errors.birthday} />

                        <AuthenticationField type="email" name="email" validate={(value: string) => validateEmail(value)} 
                            labelName={"Email"} touched={touched.email ? touched.email : false} error={errors.email} />
                        
                        <AuthenticationField type="password" name="password" validate={validatePassword} 
                            labelName={"Password"} touched={touched.password ? touched.password : false} error={errors.password}/>

                        <AuthenticationField type="password" name="confirmPassword" validate={(password: string) => validateMatching(values.password, password)} 
                            labelName={"Confirm Password"} touched={touched.confirmPassword ? touched.confirmPassword : false} error={errors.confirmPassword}/>
                        
                        <button type="submit">Register</button>
                </Form>
            )}
        </Formik>
    </div>
}

