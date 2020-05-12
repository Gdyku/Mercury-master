import React, { useEffect } from "react";
import { signout } from '../../services/auth-service'

const Logout = () => {
    useEffect(() => {
        signout();
    });

    return (
        <div>
            Callback
        </div>
      )
}

export default Logout;