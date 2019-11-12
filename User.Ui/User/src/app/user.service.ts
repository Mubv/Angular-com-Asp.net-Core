import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  url = 'https://localhost:44377/Usuario/v1';

  constructor(private http: HttpClient) {}

  getAllUser(): Observable<User[]> {
    return this.http.get<User[]>(this.url + '/Usuarios');
  }

  getUserById(Id: string): Observable<User> {
    return this.http.get<User>(this.url + '/Usuario/' + Id);
  }

  createUser(user: User): Observable<User> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.post<User>(this.url + '/UsuarioNovo/', user, httpOptions);
  }

  updateUser(user: User): Observable<User> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.put<User>(
      this.url + '/UsuarioAlterar/',
      user,
      httpOptions
    );
  }

  deleteUserById(user: User): Observable<User> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }), body: user
    };
    return this.http.delete<User>(
      this.url + '/UsuarioExcluir',
      httpOptions
    );
  }
}
