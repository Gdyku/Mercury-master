import { ADD_SCHEDULE_ERROR, ADD_SCHEDULE_PENDING } from "../constants/action-types";

const forbiddenWords = ["spam", "money"];

export function forbiddenWordsMiddleware({ dispatch }) {
    return function (next) {
        return function (action) {
            if (action.type === ADD_SCHEDULE_PENDING) {

                const foundWord = forbiddenWords.filter(word =>
                    action.payload.name.includes(word)
                );

                if (foundWord.length) {
                    return dispatch({ type: ADD_SCHEDULE_ERROR });
                }
            }
            return next(action);
        };
    };
}