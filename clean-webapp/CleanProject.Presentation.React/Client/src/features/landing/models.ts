export type AuthForm = {
    username: string;
    password: string;
}

export type RegisterForm = {
    username: string;
    password: string;
    confirmPassword: string;
    message?: string;
}

export type FormType = 'login'|'register';

export type LandingState = {
    authForm: AuthForm;
    regForm: RegisterForm;
    formType: FormType;
}

export const DefaultLandingState:LandingState = {
    authForm: {
        username: '',
        password: '',
    },
    regForm: {
        username: '',
        password: '',
        confirmPassword: ''
    },
    formType: 'login'
}

export type TokenResponseData = {
    token: string;
}

export type AuthRequestPayload = {
    email: string;
    password: string;
}