import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Hotellist } from './hotellist';

describe('Hotellist', () => {
  let component: Hotellist;
  let fixture: ComponentFixture<Hotellist>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Hotellist]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Hotellist);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
