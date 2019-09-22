import JwtDecode from 'jwt-decode';

interface TokenDto {
  foo: string;
  exp: number;
  iat: number;
}

// FROM: https://github.com/auth0/jwt-decode/issues/52#issuecomment-456703242
export function tokenIsExpired(): boolean {
  const userJson = JSON.parse(localStorage.user);
  const decodedToken = JwtDecode<TokenDto>(userJson.token);
  const currentTime = Date.now() / 1000;

  return decodedToken.exp < currentTime;
}