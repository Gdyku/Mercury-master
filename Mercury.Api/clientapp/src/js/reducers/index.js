import { ADD_SCHEDULE_PENDING, DATA_LOADED, ADD_SCHEDULE_ERROR, ADD_SCHEDULE_SUCCESS, 
    ADD_MARKER_PENDING, ADD_MARKER_ERROR, ADD_MARKER_SUCCESS,
    LOGIN_PENDING, LOGIN_SUCCESS, LOGIN_ERROR, 
    FETCH_USER_PENDING, FETCH_USER_SUCCESS, FETCH_USER_ERROR, UPDATE_USER_PENDING } from "../constants/action-types";
import { stat } from "fs";

const scheduleState = {
    schedules: [],
    isPending: false,
    isError: false,
    errorMessage: ""
};

const authState = {
    user: null,
    isPending: false,
    isError: false,
    errorMessage: ""
};

const markerState = {
    markers: [],
    isPending: false,
    isError: false,
    errorMessage: ""
};

const userState = {
    user: null,
    isPending: false,
    errorMessage: null
}

const initialState = {
    scheduleState: scheduleState,
    authState: authState,
    markerState: markerState,
    userState: userState
}

function rootReducer(state = initialState, action) {

    if (action.type === ADD_SCHEDULE_PENDING) {
        return {
            ...state, 
            scheduleState: {
                ...state.scheduleState,
                isPending: true,
                isError: false,
                errorMessage: ""
            }
        };
    }

    if (action.type === ADD_SCHEDULE_ERROR) {
        return  {
            ...state,
            scheduleState: {
                ...state.scheduleState,
                isError: true,
                errorMessage: action.payload,
                isPending: false 
            }
        };
    }

    if (action.type === ADD_SCHEDULE_SUCCESS) {
        return {
            ...state,
            scheduleState: {
                schedules: state.scheduleState
                    .schedules.concat(action.payload),
                isPending: false,
                isError: false,
                errorMessage: ""
            }
        };
    }
  
    if (action.type === LOGIN_PENDING) {
        return {
            ...state,
            authState: {
                ...state.authState,
                isPending: true,
                isError: false,
                errorMessage: ""
            }
        };
    }
  
    if (action.type === LOGIN_SUCCESS) {
        return {
            ...state,
            authState: {
                user: action.payload,
                isError: false,
                errorMessage: "",
                isPending: false 
            }
        };
    }

    if (action.type === LOGIN_ERROR) {
        return {
            ...state,
            authState : {
                ...state.authState,
                isPending: false,
                isError: true,
                errorMessage:  action.payload,
            }
        };
    }

    if (action.type === UPDATE_USER_PENDING) {
        return {
            ...state,
            userState: {
                ...state.userState,
                isPending: true,
                errorMessage: null,
            }
        };
    }

    if (action.type === FETCH_USER_PENDING) {
        return {
            ...state,
            userState: {
                ...state.userState,
                isPending: true,
                errorMessage: null,
            }
        };
    }
  
    if (action.type === FETCH_USER_SUCCESS) {
        return {
            ...state,
            userState: {
                user: action.payload,
                errorMessage: null,
                isPending: false 
            }
        };
    }

    if (action.type === FETCH_USER_ERROR) {
        return {
            ...state,
            userState : {
                ...state.userState,
                isPending: false,
                errorMessage: action.payload,
            }
        };
    }

    return state;
}

export default rootReducer;