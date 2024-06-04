export type AuthForm = {
    username: string;
    password: string;
}

export type RegisterForm = {
    username: string;
    password: string;
    confirmPassword: string;
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

export type TokenResponse = {
    token: string;
}