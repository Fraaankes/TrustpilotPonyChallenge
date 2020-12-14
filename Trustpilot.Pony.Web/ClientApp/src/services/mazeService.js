export class MazeService {

  async createNewMaze(name) {
    return await this.sendHttpRequest("POST", "maze", JSON.stringify({ "name": name }));
  }

  async get(id) {
    return await this.sendHttpRequest("GET", `maze?id=${id}`);
  }

  async move(id, direction) {
    return await this.sendHttpRequest("POST", `/move?id=${id}&direction=${direction}`);
  }

  async sendHttpRequest(method, url, body) {
    const response = await fetch(url,
      {
        method: method,
        headers: {
          "Content-Type": "application/json"
        },
        body: body
      });

    return await response.json();
  }
}