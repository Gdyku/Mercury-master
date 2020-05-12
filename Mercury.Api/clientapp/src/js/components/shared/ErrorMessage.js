import React from 'react';

const ErrorMessage = (props) => {

    return (
        <div className={`alert alert-danger ${props.isError ? "" : "hidden"}`} role="alert">
            {props.errorMessage}
        </div>
    )
}

export default ErrorMessage;