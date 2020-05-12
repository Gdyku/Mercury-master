import { takeEvery, call, put } from "redux-saga/effects";
import { ADD_SCHEDULE_PENDING, ADD_SCHEDULE_ERROR, ADD_SCHEDULE_SUCCESS,
    LOGIN_PENDING, LOGIN_ERROR, LOGIN_SUCCESS,
    FETCH_USER_PENDING, FETCH_USER_SUCCESS, FETCH_USER_ERROR, UPDATE_USER_PENDING  } from "../constants/action-types";
import axios from 'axios';

export default function* watcherSaga() {
    yield takeEvery(ADD_SCHEDULE_PENDING, workerSaga);
    yield takeEvery(FETCH_USER_PENDING, userSaga)
    yield takeEvery(UPDATE_USER_PENDING, updateUserSaga)
}

function* workerSaga(action) {

    try {
        const response = yield call(() => getData(action.payload));

        if(response.isError) {
          yield put({ type: ADD_SCHEDULE_ERROR, payload: response.errorMessage });
        }
        else {
          yield put({ type: ADD_SCHEDULE_SUCCESS, payload: response.objects });
        }
    } catch (e) {
        yield put({ type: ADD_SCHEDULE_ERROR, payload: e.message });
    }
}

function* userSaga(action) {

    try {
        const response = yield call(() => getUser(action.payload));

        console.log(response)

        if(response.errorMessage) {
          yield put({ type: FETCH_USER_ERROR, payload: response.errorMessage });
        }
        else {
          yield put({ type: FETCH_USER_SUCCESS, payload: response.object });
        }
    } catch (e) {
        yield put({ type: FETCH_USER_ERROR, payload: e.message });
    }
}

function* updateUserSaga(action) {

    try {
        const response = yield call(() => updateUser(action.payload));

        if(response.errorMessage) {
          yield put({ type: FETCH_USER_ERROR, payload: response.errorMessage });
        }
        else {
          yield put({ type: FETCH_USER_SUCCESS, payload: response.object });
        }
    } catch (e) {
        yield put({ type: FETCH_USER_ERROR, payload: e.message });
    }
}

function getData(body) {
    return fetch("https://localhost:44300/api/schedule", {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(body)
    }).then(response =>
        response.json()
    );
}

function getUser(user) {
    return axios.get("https://localhost:44300/api/user/" + user.profile.Id, {
        headers: {
          'Content-Type': 'application/json',
        },
      }).then(res => res.data);
}

function updateUser(user) {
    return axios.put("https://localhost:44300/api/user", user, {
        headers: {
            'Content-Type': 'application/json',
        }
      }).then(res => res.data);
}
