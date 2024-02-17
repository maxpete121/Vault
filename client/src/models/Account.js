export class Account {
  constructor(data) {
    this.id = data.id
    this.email = data.email
    this.name = data.name
    this.picture = data.picture
    this.coverImg = data.coverImg || 'https://i.redd.it/d2hd73xxwvaa1.jpg'
    // TODO add additional properties if needed
  }
}
