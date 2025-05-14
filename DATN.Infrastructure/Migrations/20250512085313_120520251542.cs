using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DATN.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _120520251542 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KoreaBlog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleVietSub = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    View = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VietSubContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateadBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KoreaBlog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RankQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RankQuestionName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelele = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RankQuestionId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSet_RankQuestion_RankQuestionId",
                        column: x => x.RankQuestionId,
                        principalTable: "RankQuestion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false),
                    AvatarImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1746432150/user_hbigun.png"),
                    Email = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfContributions = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false, defaultValue: 3),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ListeningQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestSetId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ListeningSoundURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ListeningScript = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RankQuestionId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeningQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListeningQuestion_RankQuestion_RankQuestionId",
                        column: x => x.RankQuestionId,
                        principalTable: "RankQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ListeningQuestion_TestSet_TestSetId",
                        column: x => x.TestSetId,
                        principalTable: "TestSet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReadingQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadingImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestSetId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RankQuestionId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingQuestion_RankQuestion_RankQuestionId",
                        column: x => x.RankQuestionId,
                        principalTable: "RankQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReadingQuestion_TestSet_TestSetId",
                        column: x => x.TestSetId,
                        principalTable: "TestSet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestSetId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_TestSet_TestSetId",
                        column: x => x.TestSetId,
                        principalTable: "TestSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RatingBlog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingBlog", x => x.Id);
                    table.CheckConstraint("CK_KoreaBlog_Rating", "[Rating] >= 1 AND [Rating] <= 5");
                    table.ForeignKey(
                        name: "FK_RatingBlog_KoreaBlog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "KoreaBlog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingBlog_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SystemLogging",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLogging", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemLogging_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestSetId = table.Column<int>(type: "int", nullable: false),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    CompletedQuestions = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    BestResults = table.Column<int>(type: "int", nullable: false),
                    FirstAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProgress_TestSet_TestSetId",
                        column: x => x.TestSetId,
                        principalTable: "TestSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProgress_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListeningAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListeningQuestionId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeningAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListeningAnswer_ListeningQuestion_ListeningQuestionId",
                        column: x => x.ListeningQuestionId,
                        principalTable: "ListeningQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReadingAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReadingQuestionId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingAnswer_ReadingQuestion_ReadingQuestionId",
                        column: x => x.ReadingQuestionId,
                        principalTable: "ReadingQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KoreaBlog",
                columns: new[] { "Id", "BlogImageUrl", "Content", "CreateadBy", "CreatedDate", "Title", "TitleVietSub", "UpdatedDate", "VietSubContent", "View" },
                values: new object[,]
                {
                    { 1, null, "윤석열 대통령 탄핵소추안 가결 이후 헌법재판소 인근에서 연일 집회가 열리고 있으나, 정문 앞은 집시법에 따라 집회가 금지되어 고요한 분위기를 유지하고 있다.", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 12, 15, 53, 12, 608, DateTimeKind.Local).AddTicks(7886), "헌재 앞 '尹 파면' 집회, 왜 정문은 조용한가?", "Tại sao trước cổng Tòa án Hiến pháp lại yên tĩnh trong khi có cuộc biểu tình yêu cầu phế truất Tổng thống Yoon Suk-yeol?", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sau khi Quốc hội Hàn Quốc thông qua nghị quyết luận tội Tổng thống Yoon Suk-yeol, các cuộc biểu tình yêu cầu phế truất ông đã diễn ra liên tục gần Tòa án Hiến pháp. Tuy nhiên, khu vực trước cổng chính của tòa án vẫn yên tĩnh do luật pháp Hàn Quốc cấm tổ chức tụ tập trong phạm vi 100 mét quanh Tòa án Hiến pháp, trừ một số trường hợp ngoại lệ.", 190 },
                    { 2, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1746903231/aaaaaaaasdadsadsadasdsadsa_wqkrqq.jpg", "국민의힘은 10일 실시된 전 당원 ARS 투표에서 후보 재선출 건이 부결됐다고 이날 11시 밝혔다. r당은 이날 후보 지위를 박탈당한 김문수 후보 대신 한덕수 후보로 당 대선 후보를 재선출하기 위한 당원 투표를 진행했다. 신동욱 국민의힘 수석대변인은 정확한 수치는 밝히지 않고 ‘근소한’ 차이로 부결됐다고만 설명했다.\r\n\r\n투표 부결에 따라 김 후보는 국민의힘 대선 후보 지위를 회복하게 됐다. 이날 새벽 대선 후보 자리를 박탈당한 지 22시간 만이다. 국민의힘은 11일 김 후보의 후보 등록 절차를 밟을 예정이다. 김 후보는 이를 사필귀정이라고 표현했다. 그는 “모든 것은 제자리로 돌아갈 것”이라며 “즉시 선대위를 출범시키고 빅텐트를 세워 반(反) 이재명(더불어민주당 대선 후보) 전선을 구축하겠다”고 했다.\r\n\r\n국민의힘 대선 후보로 추대된 예정이었던 한덕수 후보 측은 “국민과 당원의 뜻을 겸허하게 수용하겠다”며 “김문수 후보자와 국민의힘이 이번 대선에서 승리를 거두기를 진심으로 희망한다”고 했다. 그는 후보 등록 마감일(11일)까지 후보 단일화가 되지 않으면 대선에 출마하지 않겠다고 공언한만큼 불출마 수순을 밟을 것으로 보인다.\r\n\r\n국민의힘 지도부는 이날 새벽 1시께 비상대책위원회를 열고 김문수 후보의 당 대선 후보 지위를 박탈했다. 이어 이날 새벽 3~4시 후보 등록 절차를 다시 밟았는데, 무소속으로 있던 한덕수 후보만 입당해 입후보했다. 공식 선거 운동 개시(12일)를 이틀 앞두고 원내 2당이 자당 후보를 강판한 헌정사상 초유의 일이었다. 그러나 당원 투표에서 후보 재선출이 부결되면서 이 같은 시도는 무산됐다.\r\n\r\n후보 교체를 주도한 권영세 국민의힘 비상대책위원장은 사의를 표했다. 김 후보의 후보 등록 절차가 끝나는 대로 자리에서 물러날 예정이다. 그는 “경쟁력 있는 후보를 세우기 위한 충정으로 당원들의 뜻에 따라 내린 결단이었지만 결과적으로 당원 동지 동의를 얻지 못했다”며 “단일화를 이뤄내지 못한 것은 너무나 안타깝지만 이 또한 저의 부족함 때문이라고 생각한다”고 했다. 권성동 원내대표가 비대위원장 권한대행을 맡긴 하지만 당무우선권(대선후보가 당무 전반에 대한 우선적 권한을 행사한다는 국민의힘 당헌)을 앞세운 김 후보가 당권을 장악할 것으로 보인다.\r\n\r\n다만 대선이 한 달도 안 남은 상황에서 김 후보는 내홍을 수습해야 하는 과제를 안게 됐다. 후보 단일화 과정에서 국민의힘 주류 친윤(친윤석열) 지도부는 김 후보에게 ‘알량한 대선 후보 자리’, ‘한심하다’ 등 극언을 삼가치 않았다. 김 후보도 후보 자리를 뺏긴 후 ‘정치 쿠데타’라며 적개감을 드러냈다. 한동훈 전 당 대표나 안철수 의원 등 국민의힘 반윤(反윤석열) 인사들도 친윤계를 강하게 비판하며 김 후보에게 힘을 실었다. 이 같은 갈등은 대선은 물론 대선 이후 국민의힘 차기 지도부 선거에서도 이어질 것으로 예상된다.", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 12, 15, 53, 12, 608, DateTimeKind.Local).AddTicks(7897), "국힘 대선후보, 다시 김문수로…하루도 안돼 끝난 후보교체 촌극(종합)", "Ứng cử viên tổng thống của Đảng Quyền lực Nhân dân, trở lại với Kim Moon-soo… Trò hề thay đổi ứng cử viên kết thúc trong chưa đầy một ngày (toàn diện)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đảng Quyền lực Nhân dân tuyên bố lúc 11:00 sáng ngày 10 rằng động thái tái bầu ứng cử viên của đảng đã bị bác bỏ trong cuộc bỏ phiếu ARS của tất cả các thành viên đảng. Vào ngày này, Đảng R đã tổ chức một cuộc bỏ phiếu giữa các thành viên để bầu lại Han Deok-soo làm ứng cử viên tổng thống của đảng, thay thế Kim Moon-soo, người đã bị tước tư cách ứng cử. Shin Dong-wook, người phát ngôn chính của Đảng Quyền lực Nhân dân, không tiết lộ con số chính xác, chỉ giải thích rằng con số này đã bị bác bỏ với tỷ lệ \"sít sao\".\r\n\r\nSau khi phiếu bầu bị bác bỏ, Ứng cử viên Kim đã giành lại vị thế là ứng cử viên tổng thống của Đảng Quyền lực Nhân dân. Đã 22 giờ trôi qua kể từ khi ông bị tước tư cách ứng cử tổng thống vào sáng sớm nay. Đảng Quyền lực Nhân dân có kế hoạch bắt đầu quá trình đăng ký cho ứng cử viên Kim vào ngày 11. Ứng cử viên Kim mô tả đây là chuyện đương nhiên. Ông nói, “Mọi thứ sẽ trở lại đúng vị trí của nó,” và “Tôi sẽ ngay lập tức thành lập một ủy ban đặc biệt và dựng một căn lều lớn để xây dựng mặt trận chống Lee Jae-myung (ứng cử viên tổng thống của Đảng Dân chủ Hàn Quốc).”\r\n\r\nPhía ứng cử viên Han Deok-soo, người được kỳ vọng sẽ được Đảng Quyền lực Nhân dân đề cử làm ứng cử viên tổng thống, cho biết: \"Chúng tôi sẽ khiêm tốn chấp nhận ý nguyện của người dân và các đảng viên\" và \"Chúng tôi chân thành hy vọng rằng ứng cử viên Kim Moon-soo và Đảng Quyền lực Nhân dân sẽ giành chiến thắng trong cuộc bầu cử tổng thống này\". Ông dự kiến ​​sẽ thực hiện các bước để không ra tranh cử vì ông đã tuyên bố rằng ông sẽ không ra tranh cử tổng thống trừ khi có một ứng cử viên duy nhất được thành lập trước thời hạn đăng ký ứng cử viên (ngày 11).\r\n\r\nBan lãnh đạo Đảng Quyền lực Nhân dân đã tổ chức một cuộc họp của ủy ban ứng phó khẩn cấp vào khoảng 1:00 sáng ngày hôm đó và tước tư cách ứng cử viên tổng thống của đảng đối với ứng cử viên Kim Moon-soo. Sau đó, vào lúc 3-4 giờ sáng cùng ngày, quá trình đăng ký ứng cử viên được lặp lại và chỉ có ứng cử viên Han Deok-su, người trước đó ứng cử với tư cách độc lập, gia nhập đảng và ra tranh cử với tư cách là ứng cử viên. Đây là sự kiện chưa từng có trong lịch sử lập hiến khi hai đảng trong Quốc hội rút ứng cử viên của mình hai ngày trước khi chiến dịch tranh cử chính thức bắt đầu (ngày 12). Tuy nhiên, nỗ lực này đã bị hủy bỏ khi khả năng tái đắc cử của ứng cử viên bị bác bỏ trong cuộc bỏ phiếu của các thành viên đảng.\r\n\r\nKwon Young-se, chủ tịch ủy ban ứng phó khẩn cấp của Đảng Quyền lực Nhân dân, người lãnh đạo việc thay thế các ứng cử viên, đã nộp đơn từ chức. Ứng cử viên Kim có kế hoạch từ chức ngay sau khi quá trình đăng ký ứng cử hoàn tất. Ông cho biết, \"Đó là quyết định được đưa ra theo nguyện vọng của các đảng viên với sự chân thành nhằm đề cử một ứng cử viên có năng lực cạnh tranh, nhưng cuối cùng, tôi đã không nhận được sự đồng thuận của các đồng chí trong đảng\". Ông nói thêm, \"Thật không may là chúng ta không thể đạt được sự thống nhất, nhưng tôi tin rằng điều này cũng là do những thiếu sót của tôi.\" Mặc dù người đứng đầu đảng Kwon Seong-dong sẽ giữ chức quyền chủ tịch ủy ban khẩn cấp, nhưng có vẻ như ứng cử viên Kim, người ưu tiên các vấn đề của đảng (hiến chương của Đảng Quyền lực Nhân dân quy định rằng các ứng cử viên tổng thống có thẩm quyền ưu tiên đối với mọi vấn đề của đảng), sẽ nắm quyền kiểm soát đảng.\r\n\r\nTuy nhiên, khi cuộc bầu cử tổng thống chỉ còn chưa đầy một tháng nữa, ứng cử viên Kim phải đối mặt với nhiệm vụ giải quyết xung đột nội bộ. Trong quá trình thống nhất các ứng cử viên, nhóm lãnh đạo chính thống ủng hộ Yoon (ủng hộ Yoon Seok-yeol) của Đảng Quyền lực Nhân dân đã không kiềm chế được việc sử dụng ngôn ngữ cực đoan đối với ứng cử viên Kim, chẳng hạn như \"một ứng cử viên tổng thống đáng thương \" và \"thật thảm hại\". Ứng cử viên Kim cũng bày tỏ thái độ thù địch sau khi thua cuộc, gọi đó là một \"cuộc đảo chính chính trị\". Những nhân vật chống Yoon Seok-yeol từ Đảng Quyền lực Nhân dân, bao gồm cựu lãnh đạo đảng Han Dong-hoon và nhà lập pháp Ahn Cheol-soo, cũng chỉ trích mạnh mẽ phe ủng hộ Yoon và ủng hộ ứng cử viên Kim. Loại xung đột này dự kiến ​​sẽ tiếp tục không chỉ trong cuộc bầu cử tổng thống mà còn trong cuộc bầu cử lãnh đạo tiếp theo của Đảng Quyền lực Nhân dân sau cuộc bầu cử tổng thống.", 1000 },
                    { 3, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1746903467/newCoin_idcxma.jpg", "가상화폐가 상승세를 지속하는 가운데 시가총액 2위 이더리움의 급등세가 예사롭지 않다.\r\n\r\n9일(현지시간) 미국 가상화폐 거래소 코인베이스에 따르면 미 동부 시간 이날 오후 2시4분(서부 오전 11시 4분) 이더리움 1개당 가격은 24시간 전보다 15.08% 급등한 2347달러에 거래됐다.\r\n\r\n이는 같은 시간 2.27% 오른 가상화폐 대장주 비트코인이나 한국인의 매매가 가장 많은 것으로 알려져 있는 엑스알피(리플)의 5.94%보다 상승률은 크게 앞선다.\r\n\r\n솔라나와 도지코인도 각각 8.22%와 8.47% 올랐지만, 이더리움의 상승률에 미치지 못한다.\r\n\r\n이더리움은 전날에도 10% 넘게 치솟으며 2000달러선을 회복했다.\r\n\r\n일주일 전 1800달러대였던 것에 비해 30% 이상 뛰었다. 블룸버그 통신은 “이는 저금리와 투기적 과열로 코로나19 기간 당시 가상화폐 붐이 일었던 2021년 이후 가장 큰 주간 상승폭”이라고 분석했다.\r\n\r\n이더리움의 급등세는 글로벌 무역 긴장 완화와 네트워크 업그레이드에 대한 낙관론 때문으로 보인다. 미국과 영국 간 새로운 무역 협정 체결 이후 가상자산 전반에 따른 투자 심리가 개선되고 있기 때문이다.\r\n\r\n이더리움의 지속적인 기술 업그레이드에 대한 투자자들의 관심이 집중된 점도 가격을 끌어올리고 있다.\r\n\r\n이더리움은 최근 ‘펙트라(Pectra)’라는 업그레이드를 단행했다. 이번 업그레이드는 거래 수수료 인하와 네트워크 효율성 향상에 중점을 두고 있어 향후 거래 활성화에 대한 기대감이 나오고 있다. 가상화폐 분석업체 BRN의 수석 리서치 애널리스트 발랑탱 푸르니에는 “모멘텀이 강해 상승세가 지속될 가능성이 있다”며 “이번 가격 상승과 향후 정책적 지원은 (가상화폐에 대한) 새로운 투자자 유입으로 이어질 것”이라고 내다봤다.", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 12, 15, 53, 12, 608, DateTimeKind.Local).AddTicks(7900), "“일주일간 30% 이상 뛰었다”…급등세 예사롭지 않은 이 가상화폐", "“Nó đã tăng hơn 30% trong một tuần”… Tiền điện tử này đang trải qua một đợt tăng giá phi thường", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khi tiền ảo tiếp tục tăng, sự gia tăng của Ethereum, loại tiền điện tử lớn thứ hai theo vốn hóa thị trường, là điều bất thường.\r\n\r\nTheo sàn giao dịch tiền điện tử Coinbase của Hoa Kỳ, vào lúc 2:04 chiều ngày 9 (giờ địa phương), giờ miền Đông (11:04 sáng giờ Thái Bình Dương), giá 1 Ethereum được giao dịch ở mức 2.347 đô la, tăng 15,08% so với 24 giờ trước.\r\n\r\nĐây là mức tăng cao hơn nhiều so với mức tăng 2,27% trong cùng kỳ của Bitcoin, loại tiền điện tử hàng đầu, hoặc mức tăng 5,94% của XRP (Ripple), được biết đến là loại tiền điện tử được người Hàn Quốc giao dịch nhiều nhất.\r\n\r\nSolana và Dogecoin cũng tăng lần lượt 8,22% và 8,47% nhưng vẫn không bằng mức tăng của Ethereum.\r\n\r\nEthereum tăng vọt hơn 10% vào ngày hôm trước, giành lại mốc 2.000 đô la.\r\n\r\nGiá đã tăng hơn 30% so với mức 1.800 đô la của tuần trước. Bloomberg News phân tích rằng “đây là mức tăng hàng tuần lớn nhất kể từ năm 2021, khi lãi suất thấp và tình trạng đầu cơ quá nóng dẫn đến sự bùng nổ của tiền điện tử trong thời kỳ COVID-19”.\r\n\r\nSự tăng giá của Ethereum dường như được thúc đẩy bởi sự lạc quan về việc giảm bớt căng thẳng thương mại toàn cầu và nâng cấp mạng lưới. Điều này là do tâm lý đầu tư vào tài sản ảo đang được cải thiện sau khi ký kết hiệp định thương mại mới giữa Hoa Kỳ và Vương quốc Anh.\r\n\r\nSự chú ý của các nhà đầu tư tập trung vào những nâng cấp công nghệ đang diễn ra của Ethereum cũng đang đẩy giá lên cao.\r\n\r\nEthereum gần đây đã triển khai bản nâng cấp có tên là 'Pectra' . Bản nâng cấp này tập trung vào việc giảm phí giao dịch và cải thiện hiệu quả mạng lưới, nâng cao kỳ vọng về hoạt động giao dịch gia tăng trong tương lai. Valentin Fournier, nhà phân tích nghiên cứu cấp cao tại công ty phân tích tiền điện tử BRN , cho biết: \"Động lực rất mạnh và xu hướng tăng có thể sẽ tiếp tục\". “Đợt tăng giá này và chính sách hỗ trợ trong tương lai có thể sẽ thu hút thêm nhiều nhà đầu tư mới.”", 2900 },
                    { 4, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1746903794/TauSaoKim_f5tsfr.jpg", "1972년 지구 궤도를 벗어나는 데 실패한 옛 소련의 금성 탐사선 코스모스 482호가 53년간의 긴 우주 여정을 지구에서 마무리했다. 우려했던 지상 추락 피해는 없었다.\r\n\r\n러시아 연방우주공사(로스코스모스)는 코스모스 482호가 10일 오전 9시24분(한국시각 오후 3시24분) 인도양 상공 대기권에 진입해 자카르타 서쪽 해상에 떨어졌다고 발표했다. 이는 우주 물체 추적 기관들이 예상한 것과 같은 시간대다. 유럽우주국(ESA)은 코스모스 482호가 대기권 재진입 직전에 독일 상공에서 마지막으로 포착됐다고 밝혔다.\r\n\r\n원통 모양의 코스모스 482호는 무게 495kg, 지름 1m다. 이 탐사선엔 금성의 뜨거운 온도와 기압, 마찰열로부터 우주선을 보호해주는 방열판 등이 장착돼 있다. 그러나 실제로 우주선 본체가 다 타지 않은 채 떨어졌는지는 알려지지 않았다.", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 5, 12, 15, 53, 12, 608, DateTimeKind.Local).AddTicks(7901), "옛 소련 금성 탐사선, 인도양에 추락", "Tàu thăm dò sao Kim của Liên Xô cũ rơi xuống Ấn Độ Dương", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tàu thăm dò sao Kim Cosmos 482 của Liên Xô, vốn không thể rời khỏi quỹ đạo Trái Đất vào năm 1972, đã kết thúc hành trình 53 năm trong không gian để trở về Trái Đất. Vụ tai nạn không gây ra thiệt hại gì như người ta lo ngại.\r\n\r\nCơ quan Vũ trụ Liên bang Nga (Roscosmos) thông báo rằng Cosmos 482 đã đi vào bầu khí quyển trên Ấn Độ Dương lúc 9:24 sáng (3:24 chiều giờ Hàn Quốc) ngày 10 và rơi xuống biển phía tây Jakarta. Đây cũng là khung thời gian mà các cơ quan theo dõi vật thể vũ trụ mong đợi. Cơ quan Vũ trụ Châu Âu ( ESA ) cho biết Cosmos 482 được phát hiện lần cuối ở Đức ngay trước khi quay trở lại bầu khí quyển.\r\n\r\nCosmos 482 hình trụ nặng 495 kg và có đường kính 1 m. Đầu dò được trang bị tấm chắn nhiệt để bảo vệ tàu vũ trụ khỏi nhiệt độ cao, áp suất và nhiệt ma sát của sao Kim. Tuy nhiên, người ta không biết liệu phần thân chính của tàu vũ trụ có thực sự rơi xuống mà không bốc cháy hay không.", 5000 }
                });

            migrationBuilder.InsertData(
                table: "RankQuestion",
                columns: new[] { "Id", "CreatedDate", "Note", "RankQuestionName", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Câu hỏi về ngữ pháp", "Đọc Topik II 1 - 4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Xem tranh quảng cáo và chọn đáp án đúng", "Đọc Topik II 5 - 8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Xem biểu đồ, đọc bài để chọn đáp án đúng", "Đọc Topik II 9 - 12", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Sắp xếp thứ tự câu cho đúng", "Đọc Topik II 13 - 15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Chọn từ thích hợp để điền vào chỗ trống", "Đọc Topik II 16 - 20", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Đọc và chọn đáp án đúng", "Đọc Topik II 21 - 24", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Đọc đề mục tin tức và chọn đáp án giải thích đúng nhất", "Đọc Topik II 25 - 27", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Chọn câu phù hợp vào điền vào chỗ trống", "Đọc Topik II 28 - 31", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Điền nội dung đúng với đọan văn", "Đọc Topik II 32 - 34", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Chọn đáp án là nội dung chính của đoạn văn", "Đọc Topik II 35 - 38", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Chọn vị trí phù hợp để điền câu có sẵn vào", "Đọc Topik II 39 - 41", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Đọc đoạn văn dài và chọn đáp án phù hợp", "Đọc Topik II 42 - 50", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Nghe và chọn tranh đúng", "Nghe Topik II 1 - 3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Chọn câu tiếp nối cho đoạn hội thoại", "Nghe Topik II 4 - 8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Nghe và chọn hành động người nữ hoặc nam sẽ làm tiếp theo", "Nghe Topik II 9 - 12", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Nghe và chọn đáp án đúng", "Nghe Topik II 13 - 16", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Nghe và chọn suy nghĩ trọng tâm của nhân vật", "Nghe Topik II 17 - 20", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Nghe đoạn văn và chọn đáp án đúng (Gồm những đoạn văn dài và khó mức 1)", "Nghe Topik II 21 - 30", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Nghe đoạn văn và chọn đáp án đúng (Gồm những đoạn văn dài và khó mức 2)", "Nghe Topik II 31 - 50", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Nội dung nghe đề Topik I", "Nghe Topik I", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(1993), "Nội dung đọc đề Topik I", "Đọc Topik I", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedDate", "RoleName", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 12, 8, 53, 12, 611, DateTimeKind.Utc).AddTicks(3056), "SystemAdmin", new DateTime(2025, 5, 12, 8, 53, 12, 611, DateTimeKind.Utc).AddTicks(3056) },
                    { 2, new DateTime(2025, 5, 12, 8, 53, 12, 611, DateTimeKind.Utc).AddTicks(3058), "Admin", new DateTime(2025, 5, 12, 8, 53, 12, 611, DateTimeKind.Utc).AddTicks(3058) },
                    { 3, new DateTime(2025, 5, 12, 8, 53, 12, 611, DateTimeKind.Utc).AddTicks(3059), "User", new DateTime(2025, 5, 12, 8, 53, 12, 611, DateTimeKind.Utc).AddTicks(3060) }
                });

            migrationBuilder.InsertData(
                table: "ListeningQuestion",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsPublic", "ListeningScript", "ListeningSoundURL", "Question", "RankQuestionId", "TestSetId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(5788), true, "여자: 무엇을 도와 드릴까요?<br>남자: 이 지갑,누가 잃어버린 것 같아요.이 앞에 있었어요.<br>여자: 네,이쪽으로 주세요.", "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038713/C1NgheDe61_pkin3e.mp3", "다음을 듣고 알맞은 그림을 고르십시오.", 13, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(5804), true, "남자:수미야,괜찮아?많이 아프겠다.<br>여자:응,다리가 아파서 못 일어나겠어.<br>남자:그래?내가 도와줄 테니까 천천히 일어나 봐", "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038714/C2NgheDe61_rtmjmt.mp3", "다음을 듣고 알맞은 그림을 고르십시오.", 13, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(5806), true, "남자:직장인들은 점심시간을 어떻게 보낼까요?직장인의 점심시간은 한 시간이 70%였고,한 시간 삼십 분은 20%,한 시간 미만은 10%였습니다.식사 후 활동은 ‘동료와 차 마시기’가 가장 많았으며, ‘산책하기’, ‘낮잠 자기’가 뒤를 이었습니다.", "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038712/C3NgheDe61_n7u6p3.mp3", "다음을 듣고 알맞은 그림을 고르십시오.", 13, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(5808), true, "여자: 민수 씨,이번 주 모임 장소가 바뀌었대요.<br>남자: 그래요?어디로 바뀌었어요?<br>여자: ____________________________________________________", "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038711/C4NgheDe61_tt1xxh.mp3", "다음 대화를 잘 듣고 이어질 수 있는 말을 고르십시오.", 14, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(5809), true, "남자: 기차표 알아봤는데 금요일 오후 표는 없는 것 같아.<br>여자: 그럼 토요일 아침은 어때?<br>남자: ____________________________________________________", "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038713/C5NgheDe61_doyq1d.mp3", "다음 대화를 잘 듣고 이어질 수 있는 말을 고르십시오.", 14, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(5811), true, "여자:내일 발표만 끝나면 이제 이번 학기도 끝나네요.<br>남자:그러게요.수미 씨,방학 계획은 세웠어요?<br>여자: ____________________________________________________", "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038712/C6NgheDe60_qjhl1h.mp3", "다음 대화를 잘 듣고 이어질 수 있는 말을 고르십시오.", 14, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(5813), true, "남자:이번에 새로 시작한 드라마 말이야.진짜 재미있더라.<br>여자: 아,그 시골에서 할머니랑 사는 아이 이야기?<br>남자: ____________________________________________________", "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038714/C7NgheDe60_r1ao0k.mp3", "다음 대화를 잘 듣고 이어질 수 있는 말을 고르십시오.", 14, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 609, DateTimeKind.Utc).AddTicks(5815), true, "여자: 팀장님,프로그램 만족도 설문 조사를 만들어 봤는데요.확인해 주시겠어요?<br>남자:어디 봅시다.음,질문 수가 좀 많은 것 같네요.<br>여자: ____________________________________________________", "https://res.cloudinary.com/dmsi8fr0l/video/upload/v1747038714/C8NgheDe60_o19qff.mp3", "다음 대화를 잘 듣고 이어질 수 있는 말을 고르십시오.", 14, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ReadingQuestion",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsPublic", "Question", "RankQuestionId", "ReadingImageURL", "TestSetId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 610, DateTimeKind.Utc).AddTicks(9619), true, "(   )에 들어갈 가장 알맞은 것을 고르십시오. <br>나는 오래전에 설악산을 (       ).", 1, null, null, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 610, DateTimeKind.Utc).AddTicks(9620) },
                    { 2, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 610, DateTimeKind.Utc).AddTicks(9623), true, "(   )에 들어갈 가장 알맞은 것을 고르십시오. <br>새집으로 (       ) 가구를 새로 샀다.", 1, null, null, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 610, DateTimeKind.Utc).AddTicks(9624) },
                    { 3, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 610, DateTimeKind.Utc).AddTicks(9626), true, "다음 밑줄 친 부분과 의미가 비슷한 것을 고르십시오.<br>어려운 이웃을 <u>돕고자</u> 매년 봉사 활동에 참여하고 있다.", 1, null, null, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 610, DateTimeKind.Utc).AddTicks(9627) },
                    { 4, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 610, DateTimeKind.Utc).AddTicks(9629), true, "다음 밑줄 친 부분과 의미가 비슷한 것을 고르십시오.<br>지난 3년 동안 영화를 한 편 봤으니 거의 안 본 <u>셈이다</u>.", 1, null, null, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2025, 5, 12, 8, 53, 12, 610, DateTimeKind.Utc).AddTicks(9629) }
                });

            migrationBuilder.InsertData(
                table: "TestSet",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "RankQuestionId", "TestName", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đề 1 Đọc Topik II 1 - 4", new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedDate", "DateOfBirth", "Email", "FullName", "IsActive", "NumberOfContributions", "PasswordHash", "RoleId", "UpdatedDate", "VerificationToken" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1990, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "System Admin", true, 12, "$2a$11$npVGtYG.l6RPnJfJkpxWeeBp/qGZRT9KH3HX3ykioclFgUlWmHgM2", 1, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("5c0c563b-80d4-4485-9854-f6af58422601"), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2003, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "dolam180903@gmail.com", "Đỗ Quang Lâm", true, 5, "$2a$11$96G46ptujEMmOdswLzfBZOTc8Fft24v00HFipE5R0zPadf1TxGZXK", 2, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedDate", "DateOfBirth", "Email", "FullName", "IsActive", "PasswordHash", "RoleId", "UpdatedDate", "VerificationToken" },
                values: new object[] { new Guid("ea81763f-6534-448e-aa30-4112123493fb"), new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "b@gmail.com", "Trần Thị B", true, "$2a$11$96G46ptujEMmOdswLzfBZOTc8Fft24v00HFipE5R0zPadf1TxGZXK", 3, new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "ListeningAnswer",
                columns: new[] { "Id", "Content", "CreatedDate", "IsCorrect", "ListeningQuestionId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023773/C%C3%A2u_1_%C3%BD_1_luh7aq.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(7996), true, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747039219/C%C3%A2u_1_%C3%BD_2_o6ifse.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8017), false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747039219/C%C3%A2u_1_%C3%BD_3_vcw8yi.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8018), false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_1_%C3%BD_4_s0ryfi.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8019), false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_2_%C3%BD_1_msuecz.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8021), false, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023773/C%C3%A2u_2_%C3%BD_2_qgct0r.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8022), false, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_2_%C3%BD_3_bngzlb.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8023), true, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_2_%C3%BD_4_ewkro4.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8024), false, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_3_%C3%BD_1_v8melr.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8025), false, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_3_%C3%BD_2_jngpdj.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8027), false, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_3_%C3%BD_3_mr5euu.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8028), false, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "https://res.cloudinary.com/dmsi8fr0l/image/upload/v1747023774/C%C3%A2u_3_%C3%BD_4_z6zjwj.png", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8029), true, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "장소를 다시 말해 주세요", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8031), false, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "다음 모임은 안 갈 거예요", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8041), false, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "이번 주에 만나면 좋겠어요", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8042), false, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "정문 옆에 있는 식당이에요", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8044), true, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "아침 일찍 기차를 탔어", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8046), false, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "표가 없어서 아직 못 갔어", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8048), false, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "표가 있는지 한번 알아볼게", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8049), true, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "금요일 오후 표는 취소하자", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8050), false, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "발표는 늘 어렵지요", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8051), false, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "계획부터 세워 보세요", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8053), false, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "외국어 공부를 좀 할까 해요", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8054), true, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "학기가 시작되면 많이 바빠요", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8055), false, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "응.시골에서 산 적이 있어", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8056), false, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "아니.너무 지루해서 졸았어", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8061), false, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, "아니.드라마 볼 시간이 없었어", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8078), false, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, "응.두 사람 보면서 한참 웃었어.", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8080), true, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, "만족도가 높은 편입니다", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8081), false, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, "조사 결과가 나왔습니다", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8082), false, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, "프로그램이 적은 것 같습니다", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8084), false, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, "질문을 다시 정리해 보겠습니다", new DateTime(2025, 5, 12, 15, 53, 12, 609, DateTimeKind.Local).AddTicks(8085), true, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ReadingAnswer",
                columns: new[] { "Id", "Content", "CreatedDate", "IsCorrect", "ReadingQuestionId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "등산하고 싶다", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2082), false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "등산해도 된다 ", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2095), false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "등산할 것 같다", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2096), false, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "등산한 적이 있다", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2097), true, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "이사한 지", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2098), false, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "이사하거든", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2099), false, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "이사하려면 ", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2101), false, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "이사하고 나서 ", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2102), true, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "돕기 위해서", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2103), true, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "돕는 대신에", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2104), false, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "돕기 무섭게", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2105), false, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "돕는 바람에 ", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2107), false, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "본 척했다", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2108), false, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "보기 나름이다", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2109), false, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "보기 나름이다", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2110), false, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "본 거나 마찬가지이다", new DateTime(2025, 5, 12, 15, 53, 12, 611, DateTimeKind.Local).AddTicks(2111), true, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TestSetId",
                table: "Comment",
                column: "TestSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeningAnswer_ListeningQuestionId",
                table: "ListeningAnswer",
                column: "ListeningQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeningQuestion_RankQuestionId",
                table: "ListeningQuestion",
                column: "RankQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeningQuestion_TestSetId",
                table: "ListeningQuestion",
                column: "TestSetId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBlog_BlogId",
                table: "RatingBlog",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBlog_UserId",
                table: "RatingBlog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingAnswer_ReadingQuestionId",
                table: "ReadingAnswer",
                column: "ReadingQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingQuestion_RankQuestionId",
                table: "ReadingQuestion",
                column: "RankQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingQuestion_TestSetId",
                table: "ReadingQuestion",
                column: "TestSetId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemLogging_UserId",
                table: "SystemLogging",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSet_RankQuestionId",
                table: "TestSet",
                column: "RankQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_TestSetId",
                table: "UserProgress",
                column: "TestSetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_UserId",
                table: "UserProgress",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "ListeningAnswer");

            migrationBuilder.DropTable(
                name: "RatingBlog");

            migrationBuilder.DropTable(
                name: "ReadingAnswer");

            migrationBuilder.DropTable(
                name: "SystemLogging");

            migrationBuilder.DropTable(
                name: "UserProgress");

            migrationBuilder.DropTable(
                name: "ListeningQuestion");

            migrationBuilder.DropTable(
                name: "KoreaBlog");

            migrationBuilder.DropTable(
                name: "ReadingQuestion");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "TestSet");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RankQuestion");
        }
    }
}
