import { reactive } from 'vue'
import { Keeps } from './models/Keeps.js'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
/**@type {Keeps[]} */
  allKeeps: []
})
