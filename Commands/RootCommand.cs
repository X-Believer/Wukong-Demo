using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Frozen;
using WukongDemo.Data;
using Xmu.Cli;

namespace WukongDemo.Commands
{
    internal class RootCommand : CommandBase
    {
        // 商品管理
        private List<Product> prodList;
        private FrozenDictionary<string, Product> products;
        private CommandOption? autoRun;
        Random random = new Random();
        AppDbContext ctx = new AppDbContext();

        public RootCommand()
        {
            var pending = ctx.Database.GetPendingMigrations();
            ctx.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence;");

            if (pending.Any())
            {
                ctx.Database.Migrate();
            }

            prodList = [
                new Product("购物袋", "0001", "个", 0.2m),
                new Product("冰鲜带鱼", "0002", "kg", 39.60m),
                new Product("绿豆芽", "0003", "kg", 5.96m),
                new Product("三层肉", "0004", "kg", 49.8m),
                new Product("内脂豆腐", "0005", "盒", 3m),
                new Product("切片白果", "0006", "kg", 12),
                new Product("散装糕点", "0007", "kg", 19.8m),
                new Product("饼干", "0008", "袋", 12.8m)
            ];

            if (ctx.Products.Count() == 0)
            {
                ctx.Products.AddRange(prodList);
                ctx.SaveChanges();
                ctx.ChangeTracker.Clear();
            }

            products = prodList.ToFrozenDictionary(e => e.Code);
        }

        public override void Configure(CommandLineApplication app)
        {
            base.Configure(app);
            app.FullName = "添加订单";
            app.Description = "添加订单";
            autoRun = app.Option("-ar|--auto-run", "自动添加订单");
            app.OnExecute(Execute);
        }

        protected override int Execute(string[] args)
        {
            int randomInt = random.Next(100);
            Order order = new Order()
            {
                ID = randomInt,
                Total = 0  // 初始化订单总金额
            };
            bool isAutoRun = autoRun?.Value() == "on";
            string arg;

            List<(string code, decimal count)> autoData = [
                ("0001", 1),
                ("0002", 1.046m),
                ("0003", 0.494m),
                ("0004", 0.456m),
                ("0005", 4m),
                ("0006", 0.456m),
                ("0007", 0.762m),
                ("0008", 1m),
            ];

            if (isAutoRun)
            {
                foreach ((string code, decimal count) in autoData)
                {
                    Console.WriteLine($"商品> {code} {count}");
                    AddOrderItem(order, code, count);
                }
            }
            else
            {
                bool shouldLoop = true;
                do
                {
                    Console.Write("商品> ");
                    arg = Console.ReadLine() ?? string.Empty;
                    switch (arg.Split([' '], StringSplitOptions.RemoveEmptyEntries))
                    {
                        case [string code, string count]:
                            AddOrderItem(order, code, count);
                            break;
                        case [string code]:
                            AddOrderItem(order, code, 1);
                            break;
                        default:
                            shouldLoop = false;
                            break;
                    }
                } while (shouldLoop);
            }

            ctx.Orders.Add(order);

            ctx.SaveChanges();
            string ticket = order.PrintTicket();
            Console.WriteLine(ticket);
            Console.ReadKey();
            return 0;
        }

        private void AddOrderItem(Order order, string code, string count)
        {
            if (decimal.TryParse(count, out decimal c))
            {
                AddOrderItem(order, code, c);
            }
            else
            {
                Console.WriteLine($"必须输入正确的商品数目");
            }
        }

        private void AddOrderItem(Order order, string code, decimal count)
        {
            string errMsg = "";
            var product = ctx.Products.AsNoTracking().FirstOrDefault(p => p.Code == code);
            if (product != null)
            {
                ctx.Entry(product).State = EntityState.Detached;
                order.AddItem(product, count);
                errMsg = $"{order.Total:C}";
            }
            else
            {
                errMsg = "找不到对应商品";
            }

            Console.WriteLine(errMsg);
        }
    }
}
