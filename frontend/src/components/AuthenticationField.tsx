import { Field } from "formik";

export default function AuthenticationField(props: authenticationFieldProps){
    return <div className={props.className}>
        <div className = {props.className + "-label"}>
            {props.touched ? (props.error ? <label htmlFor={props.name}>{props.error}</label> : <label htmlFor={props.name}>{props.labelName}</label>) : <label htmlFor={props.name}>{props.labelName}</label>}
        </div>
        <Field type = {props.type} name = {props.name} validate={props.validate} />
    </div>
}

interface authenticationFieldProps {
    className?: string;
    type?: string;
    name: string;
    validate: Function;
    labelName: string;
    error: string | undefined;
    touched: boolean;
}