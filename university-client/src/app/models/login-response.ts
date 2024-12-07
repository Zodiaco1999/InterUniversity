import { UserLogin } from './user-login';

export class LoginResponse {
  user: UserLogin = {};
  accessToken = '';
  refreshToken = '';
}
