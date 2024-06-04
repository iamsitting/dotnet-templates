import {useEffect, useState} from "react";
import {DefaultLandingState, LandingState} from "./models";
import {registerUser, sendCredentials} from "./controllers.ts";

let globalState: LandingState = { ...DefaultLandingState };
let listeners: Function[] = [];

const setGlobalState = (newState: LandingState) => {
    globalState = newState;
    listeners.forEach(listener => listener(globalState));
};

export function useLandingStore() {
    const [state, setLocalState] = useState(globalState);

    useEffect(() => {
        const listener = (newState: LandingState) => setLocalState(newState);
        listeners.push(listener);
        return () => {
            listeners = listeners.filter(l => l !== listener);
        };
    }, []);

    const setAuthFormUsername = (username: string) => {
        setGlobalState({
            ...globalState,
            authForm: { ...globalState.authForm, username },
        });
    };

    const setAuthFormPassword = (password: string) => {
        setGlobalState({
            ...globalState,
            authForm: { ...globalState.authForm, password },
        });
    };

    const submitLoginForm = async () => {
        const payload = { ...globalState.authForm };
        setGlobalState({ ...DefaultLandingState });
        await sendCredentials(payload);
    };

    const setRegFormUsername = (username: string) => {
        setGlobalState({
            ...globalState,
            regForm: { ...globalState.regForm, username },
        });
    };

    const setRegFormPassword = (password: string) => {
        setGlobalState({
            ...globalState,
            regForm: { ...globalState.regForm, password },
        });
    };

    const setRegFormConfirmPassword = (confirmPassword: string) => {
        setGlobalState({
            ...globalState,
            regForm: { ...globalState.regForm, confirmPassword },
        });
    };

    const submitRegisterForm = async () => {
        const payload = { ...globalState.regForm };
        setGlobalState({ ...DefaultLandingState });
        await registerUser(payload);
    };

    return {
        state,
        setAuthFormUsername,
        setAuthFormPassword,
        submitLoginForm,
        setRegFormUsername,
        setRegFormPassword,
        setRegFormConfirmPassword,
        submitRegisterForm,
    };
}