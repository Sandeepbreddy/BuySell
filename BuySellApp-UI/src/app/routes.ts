import { AuthGuard } from './_guards/auth.guard';
import { ConnectionsComponent } from './connections/connections.component';
import { ChatComponent } from './chat/chat.component';
import { BuyItemsComponent } from './buy-items/buy-items.component';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';
import { SellItemsComponent } from './sell-items/sell-items.component';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'sell', component: SellItemsComponent },
      { path: 'chat', component: ChatComponent },
      { path: 'connections', component: ConnectionsComponent }
    ]
  },
  { path: 'buy', component: BuyItemsComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
