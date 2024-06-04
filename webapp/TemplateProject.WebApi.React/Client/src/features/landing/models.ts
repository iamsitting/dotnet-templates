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

export type LandingState = {
    authForm: AuthForm;
    regForm: RegisterForm;
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
    }
}

export type TokenResponseData = {
    token: string;
}

export type AuthRequestPayload = {
    email: string;
    password: string;
}