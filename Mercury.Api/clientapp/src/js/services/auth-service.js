import { UserManager } from 'oidc-client';

const config = {
    authority: "https://localhost:44301/",
    client_id: "implicitClient",
    redirect_uri: "https://localhost:3000/callback/",
    response_type: "id_token token",
    scope:"openid profile mercuryApi",
    post_logout_redirect_uri : "https://localhost:3000/",
    filterProtocolClaims: true,
    loadUserInfo: true
};

const mgr = new UserManager(config);

export const getUser = async () => {
    return await mgr.getUser();
}

export const isLoggedIn = async() => {
    var user = await getUser();

    if(!user || user.expired) {
        return false;
    }

    return true;
}

export const getClaims = async() => {
    var user = await getUser();

    if(!user || user.expired) {
        return user.profile;
    }

    return null;
}

export const getAuthorizationHeaderValue = async() => {
    var user = await getUser();

    if(!user || user.expired) {
        return `Bearer ${user.access_token}`;
    }

    return null;
}

export const startAuthentication = () => {
    try {
        return mgr.signinRedirect(); 
    }
    catch(e) {
        console.log('startAuthentication ', e)
    }
}

export const completeAuthentication = async() => {
    const user = await getUser();

    if (!user) {
        try{
            return await mgr.signinRedirectCallback();
        }
        catch(e) {
            console.log('completeAuthentication : ', e);
        }
    }

    return user;
}

export const signout = () => {
    console.log('log out')
    mgr.signoutRedirect();
}