import { ADD_SCHEDULE_PENDING, ADD_SCHEDULE_SUCCESS, ADD_SCHEDULE_ERROR, DATA_LOADED, 
    ADD_MARKER_PENDING, ADD_MARKER_SUCCESS, ADD_MARKER_ERROR, 
    LOGIN_PENDING, LOGIN_SUCCESS, LOGIN_ERROR,
    FETCH_USER_PENDING, FETCH_USER_SUCCESS, FETCH_USER_ERROR, UPDATE_USER_PENDING 
} from '../constants/action-types'


//ADD SCHEDULE
export function addSchedulePending(payload) {
    return { type: ADD_SCHEDULE_PENDING, payload }
};

export function addScheduleSuccess(payload) {
  return { type: ADD_SCHEDULE_SUCCESS, payload }
};

export function addScheduleError(payload) {
  return { type: ADD_SCHEDULE_ERROR, payload }
};

//DEPRECATED
export function getData() {
    return { type: "DATA_REQUESTED" };
}

//ADD MARKER
export function addMarkerPending(payload) {
    return { type: ADD_MARKER_PENDING, payload }
};

export function addMarkerSuccess(payload) {
  return { type: ADD_MARKER_SUCCESS, payload }
};

export function addMarkerError(payload) {
  return { type: ADD_MARKER_ERROR, payload }
};

//LOGIN
export function logInPending(payload) {
    return { type: LOGIN_PENDING, payload }
};

export function logInSuccess(payload) {
  return { type: LOGIN_SUCCESS, payload }
};

export function logInError(payload) {
  return { type: LOGIN_ERROR, payload }
};

//FETCH USERS
export function updateUserPending(payload) {
    return { type: UPDATE_USER_PENDING, payload }
};

export function fetchUserPending(payload) {
    return { type: FETCH_USER_PENDING, payload }
};

export function fetchUserSuccess(payload) {
  return { type: FETCH_USER_SUCCESS, payload }
};

export function fetchUserError(payload) {
  return { type: FETCH_USER_ERROR, payload }
};