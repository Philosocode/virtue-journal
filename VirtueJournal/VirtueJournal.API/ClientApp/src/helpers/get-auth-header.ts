export function getAuthHeader() {
  // return authorization header with jwt token
  const userJson = localStorage.getItem("user");
  if (!userJson) return {};

  const user = JSON.parse(userJson);
  if (user && user.token) {
      return { 'Authorization': 'Bearer ' + user.token };
  } else {
      return {};
  }
}